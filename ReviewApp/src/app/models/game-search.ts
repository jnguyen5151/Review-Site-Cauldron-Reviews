
export interface CardModel {
  appId: number,
  name: string,
  type: string,
  price: number,
  requiredAge: number,
  isFree: boolean,
  releaseDate: string,
  headerImage: string,
  description: string
}

export interface GenreModel {
  genreId: string,
  genre: string
}

export interface CategoryModel {
  categoryId: number,
  category: string
}

export interface PublisherModel {
  publisherid: number,
  publisher: string
}

export interface DeveloperModel {
  developerId: number,
  developer: string
}
