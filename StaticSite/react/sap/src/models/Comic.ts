export interface Comic {
    title: string;
    imageUrl: string;
    commentary: string;
    date: string;
    episodeNumber: number;
    episodeSubNumber: number | null;
    chapter: string;
    isAnimated: boolean;
    averageRating: number;
}