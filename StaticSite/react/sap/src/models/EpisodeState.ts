import { ComicMode } from "../enums/ComicMode";

export interface EpisodeState {
    ShowCommentary : boolean,
    Mode : ComicMode;
    EpisodeNumber: number;
    EpisodeDate : Date;
}