import { Chapter } from "../models/Chapter";

export interface IComicSelectorState {
    Display : boolean;

    // Current value holders
    SelectedEpisode: string;
    SelectedDate : Date;
    SelectedChapter? : string;


}