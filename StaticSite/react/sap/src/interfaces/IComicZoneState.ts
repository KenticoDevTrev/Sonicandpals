import { ComicMode } from "../enums/ComicMode";

export interface IComicZoneState {
    Mode: ComicMode;
    IncludeCommentary: boolean;
    EpisodeNumber: number;
    Date: Date;
    Episodes: Array<Episode>;
}