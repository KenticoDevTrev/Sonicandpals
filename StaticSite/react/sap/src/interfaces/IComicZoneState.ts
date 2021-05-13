import { ComicMode } from "../enums/ComicMode";
import { Episode } from "../models/Episode";

export interface IComicZoneState {
    Mode: ComicMode;
    IncludeCommentary: boolean;
    Episodes: Array<Episode>;
}