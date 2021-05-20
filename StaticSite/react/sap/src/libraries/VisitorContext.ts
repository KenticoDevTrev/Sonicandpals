// handles getting / setting the visitor context via cookies

import { ComicMode } from "../enums/ComicMode";
import { ComicState } from "../models/ComicState";

// Cookie object includes if Commentary is enabled/disabled (true default), last Mode, last Episode #, last Date
export class VisitorContext {
    CurrentEpisodeState : ComicState
    constructor() {
        // Load from breadcrumbs or default to empty
        this.CurrentEpisodeState = {
            ShowCommentary: true,
            Mode : ComicMode.Daily,
            EpisodeNumber : 0,
            EpisodeDate : new Date()
        }
    }

    SaveCookies() : void {
        // Save to cookies
    }

    SaveCommentaryContext(ShowCommentary: boolean) : void {
        this.CurrentEpisodeState.ShowCommentary = ShowCommentary;
        this.SaveCookies();
    }
    SaveModeContext(Mode: ComicMode) : void {
        this.CurrentEpisodeState.Mode = Mode;
        this.SaveCookies();
    }
    SaveEpisodeContextByNumber(EpisodeNumber: number) : void {
        this.CurrentEpisodeState.EpisodeNumber = EpisodeNumber;
        this.SaveCookies();
    }
    SaveEpisodeContextByDate(EpisodeDate: Date) : void {
        this.CurrentEpisodeState.EpisodeDate = EpisodeDate;
        this.SaveCookies();
    }
}