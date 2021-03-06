// Displays the given episodes title, entry, and commentary if visible
import React = require("react");
import { Card } from "react-bootstrap";
import { IComicDisplayProps } from "../interfaces/IComicDisplayProps";
import { ComicRating } from "./ComicRating";
export class ComicDisplay extends React.Component<IComicDisplayProps> {
    constructor(props) {
        super(props);

    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }

    formatDate = (date: Date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        return [month, day, year].join('/');
    }

    render() {
        const DateDisplay = this.formatDate(new Date(this.props.ComicToDisplay.date));
        return <div className="text-center">
            <Card>
                <Card.Header className="text-left">{DateDisplay} #{this.props.ComicToDisplay.episodeNumber} - {this.props.ComicToDisplay.chapter} - {this.props.ComicToDisplay.title}
                    <span className="float-right">
                        <ComicRating
                            AverageRating={this.props.ComicToDisplay.averageRating}
                            EpisodeNumber={this.props.ComicToDisplay.episodeNumber}
                            EpisodeSubNumber={this.props.ComicToDisplay.episodeSubNumber}
                            ErrorCallback={this.props.ErrorCallback}></ComicRating>
                        <button className="comic-share" title={"Click to share this comic."} onClick={(e) => { this.props.ShareComic(this.props.ComicToDisplay) }}></button>
                        
                        <button className={"TrackComic " + (this.props.TrackingEnabled ? "Enabled" : "")} title={"Click to " + (this.props.TrackingEnabled ? "stop" : "start") + " tracking which comic you are on. When you visit this site again, your last tracked comic will load."} onClick={(e) => { this.props.ToggleTracking() }}></button>
                    </span>
                </Card.Header>
                <Card.Body>{this.props.ComicToDisplay.isAnimated &&
                    <video controls>
                        <source src={this.props.ComicToDisplay.imageUrl.replace("~", "")} type="video/mp4" />
                    </video>
                }
                    {!this.props.ComicToDisplay.isAnimated &&
                        <img src={this.props.ComicToDisplay.imageUrl.replace("~", "")} alt={"Episode " + this.props.ComicToDisplay.episodeNumber + " - " + this.props.ComicToDisplay.title} />
                    }</Card.Body>
                <Card.Footer className="text-left"> <span className="d-inline-block" dangerouslySetInnerHTML={{
                    __html: "<strong>Commentary: </strong>" + this.props.ComicToDisplay.commentary
                }} />
                </Card.Footer>
            </Card>
        </div>
    }
}