// Simple Episode # entry and go
// includes Mode, current episode #
import React = require("react");
import { IComicSelector } from "../interfaces/IComicSelector";
export class ComicSelector extends React.Component<IComicSelector> {
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