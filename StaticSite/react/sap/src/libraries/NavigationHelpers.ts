// GetEpisodeLink, GetPreivousEpisodeLink, GetNextEpisodeLink, each taking Mode, EPisode #, Episode date.

import { ComicMode } from "../enums/ComicMode";

// Also logic possibly to intercept a link and ajax load next comics
export class NavigationHelper {
    constructor() {

    }

    GetEpisodeLink(Mode: ComicMode, EpisodeNumber : number, EpisodeDate: Date) : string {

        return "";
    }

    GetPreviousEpisodeLink(Mode: ComicMode, EpisodeNumber : number, EpisodeDate: Date) : string {

        return "";
    }

    GetNextEpisodeLink(Mode: ComicMode, EpisodeNumber : number, EpisodeDate: Date) : string {

        return "";
    }
}