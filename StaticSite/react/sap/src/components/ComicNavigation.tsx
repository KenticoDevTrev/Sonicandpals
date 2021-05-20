// Displays one of 5 nav buttons
// Takes Mode, Button Type [First, Previous, ModeSwitch, Next, Last], and the [last] episode being displayed
import React = require("react");
import { IComicNavigationProps } from "../interfaces/IComicNavigationProps";
export class EpisodeNavigation extends React.Component<IComicNavigationProps> {
    constructor(props) {
        super(props);

        // Load chapters via API
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }
    render() {
        // Get proper URL
        let NavClass : string = "";
        let NavUrl : string = "";

        // Perform switch on the NavType and adjust the class and url accordingly.

        return <div>
            Chapter Select Here
            </div>
    }
}