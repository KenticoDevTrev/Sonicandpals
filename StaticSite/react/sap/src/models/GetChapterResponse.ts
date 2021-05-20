import { Chapter } from "./Chapter";

export interface GetChaptersResponse {
    chapters: Array<Chapter>;
    error: string;
}