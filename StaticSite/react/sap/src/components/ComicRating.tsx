import React = require("react");
import { IComicRatingProps } from "../interfaces/IComicRatingProps";
import { IComicRatingState } from "../interfaces/IComicRatingState";
export class ComicRating extends React.Component<IComicRatingProps, IComicRatingState> {
    constructor(props) {
        super(props);

        this.setState( {
            AverageRating : this.props.AverageRating,
            Voted : false
        });
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            Average Rating here {this.state.AverageRating}
            </div>
    }
}