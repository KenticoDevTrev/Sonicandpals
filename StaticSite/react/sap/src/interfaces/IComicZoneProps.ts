import { ComicMode } from "../enums/ComicMode";

export interface IComicZoneProps {
    IsHomepage: Boolean;
    Mode: ComicMode;
    EpisodeNumber?: number;
    Date?: Date;
}