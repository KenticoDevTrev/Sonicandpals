import { Chapter } from "../models/Chapter";
import { Comic } from "../models/Comic";

export interface IComicShareProps {
    RefComic: Comic;
    CloseCallback: Function;
}