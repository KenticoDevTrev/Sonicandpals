import { ComicMode } from "../enums/ComicMode";
import { Comic } from "../models/Comic";


export interface IComicZoneState {
    Mode: ComicMode;
    IncludeCommentary: boolean;
    Comics: Array<Comic>;
    Error?: string | undefined;
}