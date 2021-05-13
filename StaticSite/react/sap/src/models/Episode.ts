export interface Episode {
    title:            string;
    imageUrl:         string;
    commentary:       string;
    date:             Date;
    episodeNumber:    number;
    episodeSubNumber?: number;
    chapter:          string;
    isAnimated:       boolean;
    averageRating:    number;
}
