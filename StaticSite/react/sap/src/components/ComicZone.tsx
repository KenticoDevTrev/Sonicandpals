import * as React from "react";
import ReactDOM = require("react-dom");
import { ComicMode } from "../enums/ComicMode";
import { IComicZoneProps } from "../interfaces/IComicZoneProps";
import { IComicZoneState } from "../interfaces/IComicZoneState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { Episode } from "../models/Episode";

export class ComicZone extends React.Component<IComicZoneProps, IComicZoneState> {

    ajaxHelper : AjaxHelper

    constructor(props) {
        super(props);

        this.ajaxHelper = new AjaxHelper();

        if(this.props.IsHomepage) {
            this.GetTodaysComic();
        } else {
            if(this.props.Mode == ComicMode.Episode) {
                this.GetComicsByEpisode(this.props.Mode, this.props.EpisodeNumber as number);
            } else {
                this.GetComicsByDate(this.props.Mode, this.props.Date as Date);
            }
        }
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            {this.state.Episodes.length == 0 &&
                <p>Loading...</p>
            }
            {this.state.Episodes.length > 0 &&
                <p>Display here.</p>
            }
            </div>
    }

    GetTodaysComic() {
        const EpisodesRetrieved : Array<Episode> = new Array<Episode>();
        this.state = {
            Mode : this.props.Mode,
            Episodes : EpisodesRetrieved,
            IncludeCommentary : true
        };
    }
    GetComicsByEpisode(Mode: ComicMode, EpisodeNumber: number) : void {
        const EpisodesRetrieved : Array<Episode> = new Array<Episode>();
        this.state = {
            Mode : this.props.Mode,
            Episodes : EpisodesRetrieved,
            IncludeCommentary : true
        };
    }
    GetComicsByDate(Mode: ComicMode, ComicDate: Date) : void {
        const EpisodesRetrieved : Array<Episode> = new Array<Episode>();
        this.state = {
            Mode : this.props.Mode,
            Episodes : EpisodesRetrieved,
            IncludeCommentary : true
        };
    }

    DisplayMessageClick = () => {
        /*this.setState({
            DisplayedMessage: true
        });*/
    }
}