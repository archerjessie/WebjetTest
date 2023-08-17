import React, { useEffect, useState } from 'react';

import { getMovieDetail } from '../../api/movieApi';
import { MovieDetail, MovieSummary } from '../../types';
import { View } from './view';

type Props = {
	movie: MovieSummary | null,
	handleClose: () => void
}

const MovieDetailModal: React.FC<Props> = ({ movie, handleClose }) => {
	const [movieDetail, setMovieDetail] = useState<MovieDetail>();
	const [loading, setLoading] = useState(false);

	useEffect(() => {
		async function fetchData(movie: MovieSummary) {
			setLoading(true);
			const movieDetail = await getMovieDetail(movie.filmworldId, movie.cinemaworldId);
			setLoading(false);
			if (movieDetail) {
				setMovieDetail(movieDetail);
			}
		}

		if (movie && movieDetail?.title !== movie.title) {
			fetchData(movie);
		}
	}, [movie, movieDetail?.title]);

	return <View movie={movie} handleClose={handleClose} loading={loading} movieDetail={movieDetail} />
}

export default MovieDetailModal;