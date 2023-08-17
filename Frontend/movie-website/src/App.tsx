import React, { useEffect, useState } from 'react';

import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Spinner from 'react-bootstrap/Spinner';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';

import { MovieSummary } from './types';
import { getMovieCollection } from './api/movieApi';
import MovieDetailModal from './components/MovieDetailModal';
import { AppContainer, StyledH1, ImageWrapper, CardTitle, CardWrapper, TextPrimary } from './styles';


const App: React.FC = () => {
	const [movies, setMovies] = useState<MovieSummary[] | null>(null);
	const [selectedMovie, setSelectedMovie] = useState<MovieSummary | null>(null);
	const [loading, setLoading] = useState(false);

	useEffect(() => {
		async function fetchData() {
			setLoading(true);
			const movieCollection = await getMovieCollection();
			setLoading(false);
			if (movieCollection) {
				setMovies(movieCollection);
			}
		}
		fetchData();
	}, []);

	return (
		<AppContainer>
			<StyledH1>The Best Movie Website</StyledH1>
			{loading && <Spinner animation="border" />}
			<Container>
				{!loading && !movies &&
					<TextPrimary>Movie list temporarily unavailable, try again later.</TextPrimary>}
				{!loading && !!movies &&
					<Row>
						{movies.map((movie) => (
							<Col xs={12} md={6} lg={4} key={movie.title}>
								<CardWrapper
									className="mb-4"
									border="secondary"
									bg="dark"
									text="white">
									<ImageWrapper variant="top" src={movie.poster} alt={"poster image temporarily unavailale."} />
									<Card.Body>
										<CardTitle>{movie.title}</CardTitle>
										<Button
											variant="primary"
											onClick={() => setSelectedMovie(movie)}>Check it out!</Button>
									</Card.Body>
								</CardWrapper>
							</Col>
						))}
					</Row>}
			</Container>
			<MovieDetailModal
				movie={selectedMovie}
				handleClose={() => setSelectedMovie(null)}></MovieDetailModal>
		</AppContainer>
	);
}

export default App;
