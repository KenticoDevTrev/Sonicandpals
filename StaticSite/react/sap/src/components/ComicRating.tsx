import React = require("react");
import { IComicRatingProps } from "../interfaces/IComicRatingProps";
import { IComicRatingState } from "../interfaces/IComicRatingState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { VoteRequest } from "../models/VoteRequest";
import { VoteResponse } from "../models/VoteResponse";
export class ComicRating extends React.Component<IComicRatingProps, IComicRatingState> {
    ajaxHelper: AjaxHelper;
    constructor(props) {
        super(props);

        this.ajaxHelper = new AjaxHelper();

        this.state = {
            AverageRating: this.props.AverageRating,
            Voted: false,
            CurrentHovered: 0
        };
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    castVote = (rating: number) => {
        if (this.state.Voted) {
            return;
        }
        const request: VoteRequest = {
            EpisodeNumber: this.props.EpisodeNumber,
            EpisodeSubNumber: this.props.EpisodeSubNumber,
            StarRating: rating
        }
        this.ajaxHelper.postRequest<VoteResponse>("//api.sonicandpals.com/api/Vote", request).then(response => {
            if (response.error && response.error.length > 0) {
                this.props.ErrorCallback(response.error);
            } else if (response.successful) {
                this.setState({
                    AverageRating: rating,
                    Voted: true
                });
            }
        })
    }
    hoveringOver = (rating: number) => {
        if (!this.state.Voted) {
            this.setState({
                CurrentHovered: rating
            });
        }
    }
    hoveringOut = () => {
        if(!this.state.Voted) {
            this.setState({
                CurrentHovered: 0
            });
        }
    }
    render() {
        const RatingIconArray = new Array();
        for (let r = 1; r <= 5; r++) {
            /*RatingIconArray.push(
                <span className={"rating-box box-"+(r+1)} key={(r+1)}>
                    <span  title={"Click to give this episode a "+(r+1)+"/5 rating."} className={"rating "+(this.state.AverageRating > r ? this.state.AverageRating <= r+.5 ? "half" : " full" : "")} onClick={() => this.castVote(r+1)}></span>
                </span>
            )*/
            RatingIconArray.push(
                <span key={(r)} onMouseOver={() => this.hoveringOver(r)} onMouseOut={this.hoveringOut} title={"Click to give this episode a " + (r) + "/5 rating."} className={"rating " + (this.state.AverageRating > (r-1) ? this.state.AverageRating <= (r - .5) ? " half" : " full" : "") + (!this.state.Voted && this.state.CurrentHovered >= (r) ? " hovering" : "")} onClick={() => this.castVote(r)}></span>
            )
        }

        return <span className={"rating-container" + (this.state.Voted ? " voted" : "")}>
            {RatingIconArray}
        </span>

    }
}