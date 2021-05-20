import { Comic } from "./Comic";

export interface ComicResponse {
    comics: Array<Comic>;
    date: Date;
    error: string;
}