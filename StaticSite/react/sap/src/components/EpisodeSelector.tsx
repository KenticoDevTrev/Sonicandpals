// Simple Episode # entry and go
// includes Mode, current episode #
import React = require("react");
import { IEpisodeSelector } from "../interfaces/IEpisodeSelector";
export class EpisodeSelector extends React.Component<IEpisodeSelector> {
    constructor(props) {
        super(props);

        // Load chapters via API
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            Episode Select Here
            </div>
    }
}