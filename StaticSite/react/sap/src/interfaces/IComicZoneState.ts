import { ComicDirection } from "../enums/ComicDirection";
import { ComicMode } from "../enums/ComicMode";
import { Chapter } from "../models/Chapter";
import { Comic } from "../models/Comic";


export interface IComicZoneState {
    Mode: ComicMode;
    IncludeCommentary: boolean;
    Comics: Array<Comic>;
    Error?: string | undefined;
    ShowComicSelect: boolean;
    Chapters?: Array<Chapter>;
    TrackingEnabled: boolean;
    NextComicDirection: ComicDirection;
    ShowShareScreen: boolean;
    ShareComic?: Comic;
    PortraitAlertShown: boolean;
}