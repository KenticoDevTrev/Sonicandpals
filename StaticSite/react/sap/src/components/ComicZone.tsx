import moment = require("moment");
import * as React from "react";
import ReactDOM = require("react-dom");
import { ComicMode } from "../enums/ComicMode";
import { NavigationType } from "../enums/NavigationType";
import { IComicZoneProps } from "../interfaces/IComicZoneProps";
import { IComicZoneState } from "../interfaces/IComicZoneState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { VisitorContext } from "../libraries/VisitorContext";
import { Comic } from "../models/Comic";
import { ComicResponse } from "../models/ComicResponse";
import { ComicQuery } from "../models/GetComicsRequest";
import { ComicDisplay } from "./ComicDisplay";
import {ComicNavigation } from "./ComicNavigation"
export class ComicZone extends React.Component<IComicZoneProps, IComicZoneState> {

    ajaxHelper : AjaxHelper
    visitorContext : VisitorContext

    constructor(props) {
        super(props);

        this.ajaxHelper = new AjaxHelper();
        this.visitorContext = new VisitorContext();
        
        this.state = {
            Mode: this.props.Mode,
            IncludeCommentary: this.visitorContext.CurrentEpisodeState.ShowCommentary,
            Comics: new Array<Comic>()
        };

        if(this.props.IsHomepage) {
            this.GetTodaysComic();
        } else {
            if(this.props.Mode == ComicMode.Episode) {
                this.GetComicsByEpisode(this.props.EpisodeNumber as number);
            } else {
                this.GetComicsByDate(this.props.Date as Date);
            }
        }
    }
    

    GetTodaysComic() {
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetTodaysComics").then(x => {
            if(x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                this.setState({
                    Comics: x.comics
                });
            }
        });
    }
    GetComicsByEpisode(EpisodeNumber: number) : void {
        const request : ComicQuery = {
            type : (this.state.Mode == ComicMode.Weekly ? "weekly" : "daily"),
            episodeNumber : EpisodeNumber,
            includeCommentary : this.state.IncludeCommentary
        };
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetComics", request).then(x => {
            if(x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                this.setState({
                    Comics: x.comics
                });
            }
        });
    }
    GetComicsByDate(ComicDate: Date) : void {
        let Mode = "";
        switch(this.state.Mode) {
            case ComicMode.Episode:
            case ComicMode.Daily:
                Mode = "daily";
                break;
            case ComicMode.Weekly:
                Mode = "weekly";
                break;
        }
        const request : ComicQuery = {
            type : Mode,
            date : ComicDate,
            includeCommentary : this.state.IncludeCommentary
        };
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetComics", request).then(x => {
            if(x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                this.setState({
                    Comics: x.comics
                });
            }
        });
    }

    GoToFirst = () => {
        const request : ComicQuery = {
            type : this.GetComicModeString(this.state.Mode),
            includeCommentary : this.state.IncludeCommentary
        };
        
        if(this.state.Mode == ComicMode.Episode){
            request.episodeNumber = 1;
        } else {
            let ComicDate = moment("2004-01-01");
            request.date = ComicDate.toDate()
        }
        
        this.LoadComics(request);
    }

    GoToLast = () => {
        const request : ComicQuery = {
            type : this.GetComicModeString(this.state.Mode),
            includeCommentary : this.state.IncludeCommentary
        };
        
        if(this.state.Mode == ComicMode.Episode){
            request.episodeNumber = 2786;
        } else {
            let ComicDate = moment("2011-06-19");
            request.date = ComicDate.toDate()
        }
        
        this.LoadComics(request);
    }

    GoToPrevious = () => {
        const RefComic : Comic = this.state.Comics[0];
        const request : ComicQuery = {
            type : this.GetComicModeString(this.state.Mode),
            includeCommentary : this.state.IncludeCommentary
        };
        
        if(this.state.Mode == ComicMode.Episode){
            request.episodeNumber = RefComic.episodeNumber-1;
        } else {
            let ComicDate = moment(RefComic.date);
            ComicDate = ComicDate.subtract(1, 'day');
            request.date = ComicDate.toDate()
        }
        
        this.LoadComics(request);
    }

    GoToNext = () => {
        const RefComic : Comic = this.state.Comics[this.state.Comics.length-1];
        const request : ComicQuery = {
            type : this.GetComicModeString(this.state.Mode),
            includeCommentary : this.state.IncludeCommentary
        };
        
        if(this.state.Mode == ComicMode.Episode){
            request.episodeNumber = RefComic.episodeNumber+1;
        } else {
            let ComicDate = moment(RefComic.date);
            ComicDate = ComicDate.add(1, 'day');
            request.date = ComicDate.toDate()
        }
        
        this.LoadComics(request);
    }

    LoadComics(Request : ComicQuery) {
        this.ajaxHelper.postRequest<ComicResponse>("http://api.sonicandpals.com/api/GetComics", Request).then(x => {
            if(x.error && x.error.length > 0) {
                alert(x.error);
            } else {
                this.setState({
                    Comics: x.comics
                });
            }
        });
    }

    SwitchMode = () => {
        // Do nothing, allow redirect
    }

    // ComicModeString is for request, and more indicates the # of comics returned.
    GetComicModeString(Mode: ComicMode) {
        switch(Mode) {
            case ComicMode.Daily:
            case ComicMode.Episode:
                return "daily";
            case ComicMode.Weekly:
                    return "weekly";
        }
    }

    DisplayMessageClick = () => {
        /*this.setState({
            DisplayedMessage: true
        });*/
    }

    _handleKeyDown = (event) => {
        switch( event.keyCode ) {
            case 37:
                this.GoToPrevious();
                break;
            case 39:
                this.GoToNext();
                break;
        }
    }

    componentDidMount() {
        document.addEventListener("keydown", this._handleKeyDown);

    }
    componentDidUpdate() {
        document.addEventListener("keydown", this._handleKeyDown);

    }
    render() {
        return <div>
            {this.state.Comics.length == 0 &&
                <p>Loading...</p>
            }
            {this.state.Comics.length > 0 &&
                <React.Fragment>
                <ComicDisplay ComicToDisplay={this.state.Comics[0]} ShowCommentary={this.state.IncludeCommentary}/>
                {this.state.Comics[0].episodeNumber > 1 &&
                    <React.Fragment>
                    <ComicNavigation NavType={NavigationType.First } Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.GoToFirst}  />
                    <ComicNavigation NavType={NavigationType.Previous } Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.GoToPrevious}  />
                    </React.Fragment>
                }
                {!this.props.IsHomepage && 
                    <ComicNavigation NavType={NavigationType.ModeSwitch } Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[0]} Callback={this.SwitchMode}  />
                }
                {this.state.Comics[this.state.Comics.length-1].episodeNumber < 2786 &&
                    <React.Fragment>
                    <ComicNavigation NavType={NavigationType.Next } Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[this.state.Comics.length-1]} Callback={this.GoToNext}  />
                    <ComicNavigation NavType={NavigationType.Last } Mode={this.state.Mode} ReferenceEpisode={this.state.Comics[this.state.Comics.length-1]} Callback={this.GoToLast}  />
                    </React.Fragment>
                }
                </React.Fragment>
            }
            </div>
    }
}