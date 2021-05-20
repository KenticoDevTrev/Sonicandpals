import { ComicMode } from "../enums/ComicMode";
import { Comic } from "../models/Comic";


export interface IComicNavigationProps {
    Mode: ComicMode;
    ReferenceEpisode: Comic;
    NavType: NavigationType;
}