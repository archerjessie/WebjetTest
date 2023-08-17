import styled from 'styled-components';
import Card from 'react-bootstrap/Card';

export const AppContainer = styled.div`
	background-color: slategrey;
	max-width: fit-content;
	margin: auto;
	padding: 4rem;
`;

export const StyledH1 = styled.h1`
	color: whitesmoke;
	margin-bottom: 2rem;
	padding-left: 1rem;
	font-family: fantasy;
`;

export const CardWrapper = styled(Card)`
	margin: auto;
	width: 18rem;
`;

export const ImageWrapper = styled(Card.Img)`
	height: 25rem;
`;

export const CardTitle = styled(Card.Title)`
	margin-bottom: 1rem;
	font-family: initial;
	height: 2.5rem;
`;

export const TextPrimary = styled.p`
	font-family: monospace;
`;