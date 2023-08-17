import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import App from './App'; // Update the import path accordingly
import * as movieApi from './api/movieApi';
import userEvent from '@testing-library/user-event';
import { MovieDetail, MovieSummary } from './types';

const mockMovieCollection = [
	{
		filmworldId: '123',
		cinemaworldId: '456',
		title: 'Movie 1',
		poster: 'movie1.jpg',
	},
	{
		filmworldId: '789',
		cinemaworldId: '101',
		title: 'Movie 2',
		poster: 'movie2.jpg',
	},
];

const mockMovieDetail: MovieDetail = {
	title: 'Movie 1',
	price: 9.99,
	year: '2022',
	genre: 'Action',
	director: 'John Doe',
	actors: 'Actor 1, Actor 2',
	language: 'English',
};
const mockEmptyMovieCollection = null;
jest.mock('./api/movieApi', () => ({
	getMovieCollection: jest.fn(),
	getMovieDetail: jest.fn(),
}));

describe('App', () => {
	it('renders movie cards', async () => {
		(movieApi.getMovieCollection as jest.Mock).mockResolvedValue(mockMovieCollection);

		render(<App />);

		await waitFor(() => {
			expect(screen.getByText('Movie 1')).toBeInTheDocument();
		});
		await waitFor(() => {
			expect(screen.getByText('Movie 2')).toBeInTheDocument();
		});
	});

	it('show temporarily unavailable text when movies collection is null', async () => {
		(movieApi.getMovieCollection as jest.Mock).mockResolvedValue(mockEmptyMovieCollection);

		render(<App />);

		await waitFor(() => {
			expect(screen.getByText('Movie list temporarily unavailable, try again later.')).toBeInTheDocument();
		});
	});


	it('renders movie cards and shows MovieDetailModal on button click', async () => {
		(movieApi.getMovieCollection as jest.Mock).mockResolvedValue(mockMovieCollection);
		(movieApi.getMovieDetail as jest.Mock).mockResolvedValue(mockMovieDetail);

		render(<App />);

		await screen.findByText('Movie 1');

		const checkItOutButtons = screen.getAllByText('Check it out!');

		const firstCheckItOutButton = checkItOutButtons[0];
		userEvent.click(firstCheckItOutButton);

		const movieDetailModal = await screen.findByText('$9.99');
		expect(movieDetailModal).toBeInTheDocument();
	});
});
