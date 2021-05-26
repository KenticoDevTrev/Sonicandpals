import { ComicMode } from "../enums/ComicMode";
import { Comic } from "../models/Comic";
import {NavigationType} from "../enums/NavigationType"

export interface IComicNavigationProps {
    Mode: ComicMode;
    ReferenceEpisode: Comic;
    NavType: NavigationType;
    Callback: Function;
}