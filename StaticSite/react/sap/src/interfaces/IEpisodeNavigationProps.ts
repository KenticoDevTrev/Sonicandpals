import { ComicMode } from "../enums/ComicMode";
import { Episode } from "../models/Episode";

export interface IEpisodeNavigationProps {
    Mode: ComicMode;
    ReferenceEpisode: Episode;
    NavType: NavigationType;
}