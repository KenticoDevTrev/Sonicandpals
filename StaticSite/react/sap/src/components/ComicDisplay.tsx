// Displays the given episodes title, entry, and commentary if visible
import React = require("react");
import { IComicDisplayProps } from "../interfaces/IComicDisplayProps";
export class ComicDisplay extends React.Component<IComicDisplayProps> {
    constructor(props) {
        super(props);

    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            {this.props.ComicToDisplay.date} {this.props.ComicToDisplay.chapter} - {this.props.ComicToDisplay.title}<br/>
            {this.props.ComicToDisplay.isAnimated && 
               <video controls>
                <source src={this.props.ComicToDisplay.imageUrl.replace("~", "") } type="video/mp4"/>
              </video>
            }
            {!this.props.ComicToDisplay.isAnimated &&
                <img src={this.props.ComicToDisplay.imageUrl.replace("~", "")} alt={"Episode "+this.props.ComicToDisplay.episodeNumber+" - "+this.props.ComicToDisplay.title} />
            }
            <br/>
            {this.props.ShowCommentary &&
                <p>{this.props.ComicToDisplay.commentary}</p>
            }
            Rating: {this.props.ComicToDisplay.averageRating}

            </div>
    }
}