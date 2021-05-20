export interface ComicQuery {
    type: string;
    includeCommentary: boolean;
    episodeNumber: number;
    date: Date;
}