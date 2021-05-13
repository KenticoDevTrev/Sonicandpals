// API to get Chapters, drop down
// Inputs are Mode, CurrentChapter
import React = require("react");
import { IChapterSelectorProps } from "../interfaces/IChapterSelectorProps";
export class ChapterSelector extends React.Component<IChapterSelectorProps> {
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
            Chapter Select Here
            </div>
    }
}