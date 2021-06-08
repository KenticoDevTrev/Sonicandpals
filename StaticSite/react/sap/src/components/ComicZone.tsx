import moment = require("moment");
import * as React from "react";
import ReactDOM = require("react-dom");
import { CSSTransition, SwitchTransition, TransitionGroup } from "react-transition-group";
import { ComicDirection } from "../enums/ComicDirection";
import { ComicMode } from "../enums/ComicMode";
import { NavigationType } from "../enums/NavigationType";
import { IComicZoneProps } from "../interfaces/IComicZoneProps";
import { IComicZoneState } from "../interfaces/IComicZoneState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { VisitorContext } from "../libraries/VisitorContext";
import { Comic } from "../models/Comic";
import { ComicResponse } from "../models/ComicResponse";
import { GetChaptersResponse } from "../models/GetChapterResponse";
import { ComicQuery } from "../models/GetComicsRequest";
import { ComicDisplay } from "./ComicDisplay";
import { ComicNavigation } from "./ComicNavigation"
import { ComicSelector } from "./ComicSelector";
export class ComicZone extends React.Component<IComicZoneProps, IComicZoneState> {

    ajaxHelper: AjaxHelper
    visitorContext: VisitorContext
    constructor(props) {
        super(props);

        this.ajaxHelper = new AjaxHelper();
        this.visitorContext = new VisitorContext();

        this.state = {
            Mode: this.props.Mode,
            IncludeCommentary: this.visitorContext.CurrentEpisodeState.ShowCommentary,
            Comics: new Array<Comic>(),
            ShowComicSelect: false,
            TrackingEnabled: this.visitorContext.trackEpisode(),
            NextComicDirection: ComicDirection.Unknown
        };

        let LoadingByContext = false;
        if (this.visitorContext.trackingAllowed() && this.visitorContext.CurrentEpisodeState.EpisodeNumber > 0) {
            LoadingByContext = true;
            if (this.visitorContext.CurrentEpisodeState.Mode == ComicMode.Episode) {
                this.GetComicsByEpisode(this.visitorContext.CurrentEpisodeState.EpisodeNumber);
            } else {
                this.GetComicsByDate(this.visitorContext.CurrentEpisodeState.EpisodeDate);
            }
        }

        if (!LoadingByContext) {
            if (this.props.IsHomepage) {
                this.GetTodaysComic();
            } else {
                if (this.props.Mode == ComicMode.Episode) {
                    this.GetComicsByEpisode(this.props.EpisodeNumber as number);
                } else {
                    this.GetComicsByDate(this.props.Date as Date);
                }
            }
        }

        // Get Chapters 
        this.GetChapters();
    }

    GetChapters = () => {
        this.ajaxHelper.postRequest<GetChaptersResponse>("http://api.sonicandpals.com/api/GetChapters").then(response => {
            if (response.error) {
                this.DisplayError(response.error);
            } else {
                this.setState({
                    Chapters: response.chapters
                });
            }
        });
    }

    GetTodaysComic = () => {
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetTodaysComics").then(x => {
            if (x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                this.setState({
                    Comics: x.comics
                });
            }
        });
    }
    GetComicsByEpisode = (EpisodeNumber: number, Direction?: ComicDirection): void => {
        const request: ComicQuery = {
            type: (this.state.Mode == ComicMode.Weekly ? "weekly" : "daily"),
            episodeNumber: EpisodeNumber,
            includeCommentary: this.state.IncludeCommentary
        };
        this.LoadComics(request, (Direction != undefined ? Direction : ComicDirection.Unknown));
    }
    GetComicsByDate = (ComicDate: Date, Direction?: ComicDirection): void => {
        let Mode = "";
        switch (this.state.Mode) {
            case ComicMode.Episode:
            case ComicMode.Daily:
                Mode = "daily";
                break;
            case ComicMode.Weekly:
                Mode = "weekly";
                break;
        }
        const request: ComicQuery = {
            type: Mode,
            date: ComicDate,
            includeCommentary: this.state.IncludeCommentary
        };
        this.LoadComics(request, (Direction != undefined ? Direction : ComicDirection.Unknown));
    }

    GoToFirst = () => {
        const request: ComicQuery = {
            type: this.GetComicModeString(this.state.Mode),
            includeCommentary: this.state.IncludeCommentary
        };

        if (this.state.Mode == ComicMode.Episode) {
            request.episodeNumber = 1;
        } else {
            let ComicDate = moment("2004-01-01");
            request.date = ComicDate.toDate()
        }

        this.LoadComics(request, ComicDirection.Unknown);
    }

    GoToLast = () => {
        const request: ComicQuery = {
            type: this.GetComicModeString(this.state.Mode),
            includeCommentary: this.state.IncludeCommentary
        };

        if (this.state.Mode == ComicMode.Episode) {
            request.episodeNumber = 2786;
        } else {
            let ComicDate = moment("2011-06-19");
            request.date = ComicDate.toDate()
        }

        this.LoadComics(request, ComicDirection.Unknown);
    }

    GoToPrevious = () => {
        const RefComic: Comic = this.state.Comics[0];
        if (RefComic.episodeNumber == 1) {
            return;
        }
        const request: ComicQuery = {
            type: this.GetComicModeString(this.state.Mode),
            includeCommentary: this.state.IncludeCommentary
        };

        if (this.state.Mode == ComicMode.Episode) {
            request.episodeNumber = RefComic.episodeNumber - 1;
        } else {
            let ComicDate = moment(RefComic.date);
            ComicDate = ComicDate.subtract(1, 'day');
            request.date = ComicDate.toDate()
        }

        this.LoadComics(request, ComicDirection.Backward);
    }

    GoToNext = () => {
        const RefComic: Comic = this.state.Comics[this.state.Comics.length - 1];
        if (RefComic.episodeNumber == 2786) {
            return;
        }
        const request: ComicQuery = {
            type: this.GetComicModeString(this.state.Mode),
            includeCommentary: this.state.IncludeCommentary
        };

        if (this.state.Mode == ComicMode.Episode) {
            request.episodeNumber = RefComic.episodeNumber + 1;
        } else {
            let ComicDate = moment(RefComic.date);
            ComicDate = ComicDate.add(1, 'day');
            request.date = ComicDate.toDate()
        }

        this.LoadComics(request, ComicDirection.Forward);
    }

    ShowComicSelect = () => {
        this.setState({
            ShowComicSelect: true
        });
    }

    HideComicSelect = () => {
        this.setState({
            ShowComicSelect: false
        })
    }

    LoadComics = (Request: ComicQuery, Direction: ComicDirection) => {
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetComics", Request).then(x => {
            if (x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                if (x.comics.length == 0) {
                    // Proceed another step in that direction
                    if (Direction != ComicDirection.Unknown) {
                        const Increment = Direction == ComicDirection.Forward!;
                        if (Request.episodeNumber) {
                            const NextNumber = (Request.episodeNumber!) + (Increment ? 1 : -1);
                            if (NextNumber > 2786 || NextNumber < 1) {
                                this.setState({
                                    Error: "You are at the " + (Increment ? "end" : "beginning") + " of the comic."
                                })
                            } else {
                                this.GetComicsByEpisode(NextNumber, Direction);
                            }
                        } else if (Request.date) {
                            let ComicDate = moment(Request.date!);
                            ComicDate = ComicDate.add((Increment ? 1 : -1), 'day');
                            if (ComicDate.toDate() < new Date(2004, 0, 1) || ComicDate.toDate() > new Date(2011, 5, 19)) {
                                this.setState({
                                    Error: "You are at the " + (Increment ? "end" : "beginning") + " of the comic."
                                });
                            }
                            this.GetComicsByDate(ComicDate.toDate(), Direction);
                        }
                    } else {
                        // Selected invalid comic
                        this.setState({
                            Error: "No Comics Found for your requested " + ((Request.episodeNumber) ? "episode" : "date") + "."
                        })
                    }
                } else {
                    if (this.visitorContext.trackEpisode()) {
                        this.visitorContext.saveEpisodeContext(x.comics[0]);
                        this.visitorContext.saveCookies();
                    }
                    this.setState({
                        Comics: x.comics,
                        Error: undefined!,
                        NextComicDirection: Direction
                    });
                }
            }
        });
    }

    // ComicModeString is for request, and more indicates the # of comics returned.
    GetComicModeString = (Mode: ComicMode) => {
        switch (Mode) {
            case ComicMode.Daily:
            case ComicMode.Episode:
                return "daily";
            case ComicMode.Weekly:
                return "weekly";
        }
    }

    ToggleTracking = () => {
        if (!this.visitorContext.trackingAllowed()) {
            // Get permission to enable tracking
            if (window.confirm("Cookies are required to track your current comic state.  And honestly, that's all we use the cookies for.  By clicking 'Okay' you agree to enable this feature.")) {
                this.visitorContext.allowTracking();
            } else {
                return;
            }
        }

        if (this.visitorContext.trackEpisode()) {
            this.visitorContext.endTracking();
        } else {
            this.visitorContext.startTracking();
            this.visitorContext.saveEpisodeContext(this.state.Comics[0]);
        }
        this.visitorContext.saveCookies();
        this.setState({
            TrackingEnabled: this.visitorContext.trackEpisode()
        })
    }

    DisplayError = (error: string) => {
        /*this.setState({
            DisplayedMessage: true
        });*/
    }

    handleKeyDown = (event) => {
        switch (event.keyCode) {
            case 37:
                this.GoToPrevious();
                break;
            case 39:
                this.GoToNext();
                break;
        }
    }

    handleTouchStart = (event) => {
        switch (event.keyCode) {
            case 37:
                this.GoToPrevious();
                break;
            case 39:
                this.GoToNext();
                break;
        }
    }

    handleSwipeLeft = (event) => {
        console.log("Swiped Left");
        this.GoToNext();
    }
    handleSwipeRight = (event) => {
        console.log("Swiped Right");
        this.GoToPrevious();
    }

    componentDidMount() {
        document.addEventListener("keydown", this.handleKeyDown);
        document.addEventListener('swiped-left', this.handleSwipeLeft);
        document.addEventListener('swiped-right', this.handleSwipeRight);

    }
    componentDidUpdate() {
        document.addEventListener("keydown", this.handleKeyDown);
        document.addEventListener('swiped-left', this.handleSwipeLeft);
        document.addEventListener('swiped-right', this.handleSwipeRight);
    }


    render() {
        var ComicsList = this.state.Comics.map(function (comic, i) {
            //@ts-ignore
            return <ComicDisplay key={comic.episodeNumber} ComicToDisplay={comic} ShowCommentary={this.state.IncludeCommentary} ToggleTracking={this.ToggleTracking} TrackingEnabled={this.state.TrackingEnabled} />;
        }, this);

        let TransitionClass = "fade";
        /*switch (this.state.NextComicDirection) {
            case ComicDirection.Forward:
                TransitionClass = "forward";
                break;
            case ComicDirection.Backward:
                TransitionClass = "backward";
                break;
            case ComicDirection.Unknown:
                TransitionClass = "fade";
                break;
        }*/
        return <div>
            {this.state.Comics.length == 0 &&
                <p>{typeof this.state.Error != "undefined" ? this.state.Error : "Loading..."}</p>
            }
            {this.state.Comics.length > 0 &&
                <React.Fragment>

                    <div className="text-center">
                        <div className="comic-container">
                            <SwitchTransition>
                                <CSSTransition key={this.state.Comics[0].episodeNumber} timeout={250} classNames={"comic-transition-"+TransitionClass}>
                                    <React.Fragment>
                                        {ComicsList}
                                    </React.Fragment>
                                </CSSTransition>
                            </SwitchTransition>
                        </div>
                        <div className="row">
                            <div className="col-12 text-center">
                                <ul className="EpisodeNavigation d-inline-block">
                                    {this.state.Comics[0].episodeNumber > 1 &&
                                        <React.Fragment>
                                            <ComicNavigation NavType={NavigationType.First} Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.GoToFirst} />
                                            <ComicNavigation NavType={NavigationType.Previous} Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.GoToPrevious} />
                                        </React.Fragment>
                                    }
                                    {this.state.Chapters &&
                                        <ComicNavigation NavType={NavigationType.ModeSwitch} Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.ShowComicSelect} />
                                    }
                                    {this.state.Comics[this.state.Comics.length - 1].episodeNumber < 2786 &&
                                        <React.Fragment>
                                            <ComicNavigation NavType={NavigationType.Next} Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[this.state.Comics.length - 1]} Callback={this.GoToNext} />
                                            <ComicNavigation NavType={NavigationType.Last} Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[this.state.Comics.length - 1]} Callback={this.GoToLast} />
                                        </React.Fragment>
                                    }
                                </ul>
                            </div>
                        </div>
                    </div>

                </React.Fragment>
            }
            {this.state.ShowComicSelect &&
                <ComicSelector Chapters={this.state.Chapters} GoToDate={this.GetComicsByDate} GoToEpisode={this.GetComicsByEpisode} CloseCallback={this.HideComicSelect} RefComic={this.state.Comics.length > 0 ? this.state.Comics[this.state.Comics.length - 1] : null}></ComicSelector>
            }
        </div>
    }
}