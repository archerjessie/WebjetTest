import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Spinner from 'react-bootstrap/Spinner';
import Stack from 'react-bootstrap/Stack';

import { MovieDetail, MovieSummary } from '../../types';
import { ModalHeader, ModalBody, TextFieldPrimary } from './styles';

type Props = {
	movie: MovieSummary | null,
	handleClose: () => void,
	movieDetail?: MovieDetail,
	loading: boolean
}

export const View: React.FC<Props> = ({ movie, handleClose, movieDetail, loading }) => {
	return (
		<Modal show={!!movie} onHide={handleClose}>
			<ModalHeader closeButton>
				<Modal.Title>{movie?.title}</Modal.Title>
			</ModalHeader>
			<ModalBody>
				{loading && <Spinner animation="border" />}
				{!loading && !movieDetail &&
					<TextFieldPrimary>Movie detail temporarily unavailable, try again later.</TextFieldPrimary>}
				{!loading && !!movieDetail &&
					<Stack gap={3}>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Language</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">{movieDetail?.language}</TextFieldPrimary>
						</Stack>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Year</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">{movieDetail?.year}</TextFieldPrimary>
						</Stack>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Genre</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">{movieDetail?.genre}</TextFieldPrimary>
						</Stack>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Director</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">{movieDetail?.director}</TextFieldPrimary>
						</Stack>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Actors</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">{movieDetail?.actors}</TextFieldPrimary>
						</Stack>
						<Stack direction="horizontal" gap={3}>
							<TextFieldPrimary className="p-2">Best price</TextFieldPrimary>
							<TextFieldPrimary className="p-2 ms-auto">${movieDetail?.price}</TextFieldPrimary>
						</Stack>
					</Stack>
				}
			</ModalBody>
		</Modal>
	)
}