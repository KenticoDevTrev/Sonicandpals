// handles getting / setting the visitor context via cookies
import { ComicMode } from "../enums/ComicMode";
import { Comic } from "../models/Comic";
import { ComicState } from "../models/ComicState";

// Cookie object includes if Commentary is enabled/disabled (true default), last Mode, last Episode #, last Date
export class VisitorContext {
    CurrentEpisodeState: ComicState
    constructor() {
        let TrackingAllowedCookie = this.getCookie("EpisodeTrackingAllowed");

        if (TrackingAllowedCookie != null && TrackingAllowedCookie == "true" && this.getCookie("EpisodeNumber")) {
            // Load state
            try {
                this.CurrentEpisodeState = {
                    ShowCommentary: this.getCookie("ShowCommentary") == "true",
                    Mode: this.getCookie("Mode") == "Episode" ? ComicMode.Episode : ComicMode.Daily,
                    EpisodeNumber: parseInt(this.getCookie("EpisodeNumber")!),
                    EpisodeDate: new Date(this.getCookie("EpisodeDate")!),
                    TrackingAllowed : this.getCookie("EpisodeTrackingAllowed") == "true",
                    TrackingEpisode : this.getCookie("TrackingEpisode") == "true"
                }
            } catch (ex) {
                this.CurrentEpisodeState = {
                    ShowCommentary: true,
                    Mode: ComicMode.Episode,
                    EpisodeNumber: 0,
                    EpisodeDate: new Date(),
                    TrackingAllowed : TrackingAllowedCookie != null && TrackingAllowedCookie == "true",
                    TrackingEpisode : false

                }
            }
        } else {

            // Load from breadcrumbs or default to empty
            this.CurrentEpisodeState = {
                ShowCommentary: true,
                Mode: ComicMode.Episode,
                EpisodeNumber: 0,
                EpisodeDate: new Date(),
                TrackingAllowed : TrackingAllowedCookie != null && TrackingAllowedCookie == "true",
                TrackingEpisode : false
            }
        }
    }

    saveCookies(): void {
        // Save to cookies
        document.cookie = "ShowCommentary=" + (this.CurrentEpisodeState.ShowCommentary ? "true" : "false")+";max-age=31536000";
        document.cookie = "Mode=" + (this.CurrentEpisodeState.Mode == ComicMode.Episode ? "Episode" : "Daily")+";max-age=31536000";
        document.cookie = "EpisodeNumber=" + this.CurrentEpisodeState.EpisodeNumber+";max-age=31536000";
        document.cookie = "EpisodeDate=" + this.formatDate(this.CurrentEpisodeState.EpisodeDate)+";max-age=31536000";
        document.cookie =  "EpisodeTrackingAllowed="+(this.CurrentEpisodeState.TrackingAllowed ? "true" :"false")+";max-age=31536000";
        document.cookie =  "TrackingEpisode="+(this.CurrentEpisodeState.TrackingEpisode ? "true" :"false")+";max-age=31536000";
    }

    // This allows tracking to be done
    allowTracking(): void {
        this.CurrentEpisodeState.TrackingAllowed = true;
        this.saveCookies();
    }

    // This is if tracking is allowed
    trackingAllowed(): boolean {
        return this.CurrentEpisodeState.TrackingAllowed;
    }


    // this will turn on tracking, so when they move to another comic it will keep track of where they are
    startTracking(): void {
        this.CurrentEpisodeState.TrackingEpisode = true;
        this.saveCookies();
    }

    // This will disable tracking, if they want to go to another comic or something.
    endTracking(): void {
        this.CurrentEpisodeState.TrackingEpisode = false;
        this.saveCookies();
    }

    // This will return true if tracking is on, will be used to trigger the save methods on changes.
    trackEpisode(): boolean {
        return this.CurrentEpisodeState.TrackingEpisode;
    }

    saveCommentaryContext(ShowCommentary: boolean): void {
        this.CurrentEpisodeState.ShowCommentary = ShowCommentary;
        this.saveCookies();
    }
    saveModeContext(Mode: ComicMode): void {
        this.CurrentEpisodeState.Mode = Mode;
        this.saveCookies();
    }
    saveEpisodeContext(comic: Comic): void {
        this.CurrentEpisodeState.EpisodeNumber = comic.episodeNumber;
        this.CurrentEpisodeState.EpisodeDate = new Date(comic.date);
        this.saveCookies();
    }

    private getCookie(name: string): string | null {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) {
            //@ts-ignore
            return parts.pop().split(';').shift();
        } else {
            return null;
        }
    }
    private formatDate = (date: Date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }
}