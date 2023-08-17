import { MovieDetail, MovieSummary } from '../types'
import { fetchData } from './apiUtils'

const baseUrl = process.env.REACT_APP_API_URL + '/api/movies/'

export async function getMovieCollection(): Promise<MovieSummary[] | null> {
	return await fetchData<MovieSummary[]>(baseUrl)
}

export async function getMovieDetail(fwid: string, cwid: string): Promise<MovieDetail | null> {
	return await fetchData<MovieDetail>(`${baseUrl}detail/?fwid=${fwid}&cwid=${cwid}`)
}