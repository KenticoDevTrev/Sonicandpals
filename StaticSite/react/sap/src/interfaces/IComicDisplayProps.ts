import { Comic } from "../models/Comic";

export interface IComicDisplayProps {
    ComicToDisplay: Comic;
    ShowCommentary: boolean;
}