import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import MovieDetailModal from '.';
import * as movieApi from '../../api/movieApi';
import { MovieDetail, MovieSummary } from '../../types';

const mockMovie: MovieSummary = {
	filmworldId: '123',
	cinemaworldId: '456',
	title: 'Test Movie',
	poster: 'test.jpg',
};

const mockMovieDetail: MovieDetail = {
	title: 'Test Movie',
	price: 9.99,
	year: '2022',
	genre: 'Action',
	director: 'John Doe',
	actors: 'Actor 1, Actor 2',
	language: 'English',
};

jest.mock('../../api/movieApi', () => ({
	getMovieDetail: jest.fn(),
}));

describe('MovieDetailModal', () => {
	it('renders movie details', async () => {
		(movieApi.getMovieDetail as jest.Mock).mockResolvedValue(mockMovieDetail);

		render(<MovieDetailModal movie={mockMovie} handleClose={() => { }} />);

		await waitFor(() => {
			expect(screen.getByText('Test Movie')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('$9.99')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('2022')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('Action')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('John Doe')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('Actor 1, Actor 2')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('English')).toBeInTheDocument();
		});
	});

	it('handles error when fetching movie details', async () => {
		(movieApi.getMovieDetail as jest.Mock).mockResolvedValue(null);

		render(<MovieDetailModal movie={mockMovie} handleClose={() => { }} />);

		await waitFor(() => {
			expect(screen.getByText('Test Movie')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText("Movie detail temporarily unavailable, try again later.")).toBeInTheDocument();
		});
	});
});
