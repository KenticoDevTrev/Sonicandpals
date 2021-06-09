// Displays one of 5 nav buttons
// Takes Mode, Button Type [First, Previous, ModeSwitch, Next, Last], and the [last] episode being displayed
import React = require("react");
import { ComicMode } from "../enums/ComicMode";
import { NavigationType } from "../enums/NavigationType";
import { IComicNavigationProps } from "../interfaces/IComicNavigationProps";
export class ComicNavigation extends React.Component<IComicNavigationProps> {
    constructor(props) {
        super(props);

        this.handleClick = this.handleClick.bind(this);

    }
    handleClick(e) {
        e.stopPropagation();
        e.nativeEvent.stopImmediatePropagation();
        this.props.Callback();
    }

    formatDate = (date: Date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }

    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        // Get proper URL
        let NavClass: string = "";
        let NavUrl: string = "/";
        /*switch (this.props.Mode) {
            case ComicMode.Daily:
                NavUrl = "/Daily.html";
                break;
            case ComicMode.Episode:
                NavUrl = "/Episode.html";
                break;
            case ComicMode.Weekly:
                NavUrl = "/Weekly.html";
        }*/
        switch (this.props.NavType) {
            case NavigationType.First:
                NavClass = "Nav-First";
                switch (this.props.Mode) {
                    case ComicMode.Daily:
                        NavUrl += "?Date=2004-01-01";
                        break;
                    case ComicMode.Weekly:
                        NavUrl += "?Date=2004-01-01";
                        break;
                    case ComicMode.Episode:
                        NavUrl += "?Episode=1";
                        break;
                }
                break;
            case NavigationType.Last:
                NavClass = "Nav-Last";
                switch (this.props.Mode) {
                    case ComicMode.Daily:
                        NavUrl += "?Date=2011-06-19";
                        break;
                    case ComicMode.Weekly:
                        NavUrl += "?Date=2011-06-19";
                        break;
                    case ComicMode.Episode:
                        NavUrl += "?Episode=2786";
                        break;
                }
                break;
            case NavigationType.ModeSwitch:
                NavClass = "Nav-ComicSelect";
                NavUrl += "";
                break;
            case NavigationType.Next:
                NavClass = "Nav-Next";
                switch (this.props.Mode) {
                    case ComicMode.Daily:
                    case ComicMode.Weekly:
                        // Increment episode by 1 date
                        let ComicDate = new Date(this.props.ReferenceEpisode.date);
                        ComicDate.setDate(ComicDate.getDate() + 1);
                        NavUrl += "?Date=" + this.formatDate(ComicDate);
                        break;
                    case ComicMode.Episode:
                        NavUrl += "?Episode=" + (this.props.ReferenceEpisode.episodeNumber + 1);
                        break;
                }
                break;
            case NavigationType.Previous:
                NavClass = "Nav-Previous";
                switch (this.props.Mode) {
                    case ComicMode.Daily:
                    case ComicMode.Weekly:
                        // Increment episode by 1 date
                        let ComicDate = new Date(this.props.ReferenceEpisode.date);
                        ComicDate.setDate(ComicDate.getDate() - 1);
                        NavUrl += "?Date=" + this.formatDate(ComicDate);
                        break;
                    case ComicMode.Episode:
                        NavUrl += "?Episode=" + (this.props.ReferenceEpisode.episodeNumber - 1);
                        break;
                }
                break;
        }

        // Perform switch on the NavType and adjust the class and url accordingly.
        return <li>
            <a onClick={this.handleClick} className={"Navigation-Item " + NavClass}></a>
        </li>
    }
}