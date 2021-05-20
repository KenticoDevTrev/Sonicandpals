import React = require("react");
import { IComicCalendarProps } from "../interfaces/IComicCalendarProps";
// Calendar display with weekly sidebar
// Inputs are Mode, Current Date (1st day of month + year)
export class ComicCalendar extends React.Component<IComicCalendarProps> {
    constructor(props) {
        super(props);

    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        return <div>
            Calendar Here
            </div>
    }
}