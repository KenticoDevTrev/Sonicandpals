import { Chapter } from "../models/Chapter";
import { Comic } from "../models/Comic";

export interface IComicSelectorProps {
    RefComic: Comic | null;
    CloseCallback: Function;
    GoToEpisode(EpisodeNumber : number) : void;
    GoToDate(ComicDate: Date) : void;
    Chapters? : Array<Chapter>;
}