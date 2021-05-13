// Displays the given episodes title, entry, and commentary if visible
import React = require("react");
import { IEpisodeDisplayProps } from "../interfaces/IEpisodeDisplayProps";
export class ChapterSelector extends React.Component<IEpisodeDisplayProps> {
    constructor(props) {
        super(props);

    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            {this.props.EpisodeToDisplay.date} {this.props.EpisodeToDisplay.chapter} - {this.props.EpisodeToDisplay.title}<br/>
            {this.props.EpisodeToDisplay.isAnimated && 
               <video controls>
                <source src={this.props.EpisodeToDisplay.imageUrl } type="video/mp4"/>
              </video>
            }
            {!this.props.EpisodeToDisplay.isAnimated &&
                <img src={this.props.EpisodeToDisplay.imageUrl} alt={"Episode "+this.props.EpisodeToDisplay.episodeNumber+" - "+this.props.EpisodeToDisplay.title} />
            }
            <br/>
            {this.props.ShowCommentary &&
                <p>{this.props.EpisodeToDisplay.commentary}</p>
            }
            Rating: {this.props.EpisodeToDisplay.averageRating}

            </div>
    }
}