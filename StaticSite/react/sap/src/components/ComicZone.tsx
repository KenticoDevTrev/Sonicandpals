import * as React from "react";
import ReactDOM = require("react-dom");
import { ComicMode } from "../enums/ComicMode";
import { IComicZoneProps } from "../interfaces/IComicZoneProps";
import { IComicZoneState } from "../interfaces/IComicZoneState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { VisitorContext } from "../libraries/VisitorContext";
import { Comic } from "../models/Comic";
import { ComicResponse } from "../models/ComicResponse";
import { ComicQuery } from "../models/GetComicsRequest";
import { ComicDisplay } from "./ComicDisplay";

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
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            {this.state.Comics.length == 0 &&
                <p>Loading...</p>
            }
            {this.state.Comics.length > 0 &&
                <ComicDisplay ComicToDisplay={this.state.Comics[0]} ShowCommentary={this.state.IncludeCommentary}/>
            }
            </div>
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

    DisplayMessageClick = () => {
        /*this.setState({
            DisplayedMessage: true
        });*/
    }
}