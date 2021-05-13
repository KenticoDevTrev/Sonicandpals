import React = require("react");
import { IEpisodeCalendarProps } from "../interfaces/IEpisodeCalendarProps";
// Calendar display with weekly sidebar
// Inputs are Mode, Current Date (1st day of month + year)
export class EpisodeCalendar extends React.Component<IEpisodeCalendarProps> {
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