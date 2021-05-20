import { ComicMode } from "../enums/ComicMode";

export interface ComicState {
    ShowCommentary : boolean,
    Mode : ComicMode;
    EpisodeNumber: number;
    EpisodeDate : Date;
}