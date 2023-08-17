import axios, { AxiosResponse } from 'axios';

export async function fetchData<T>(url: string): Promise<T | null> {
	try {
		const response: AxiosResponse<T> = await axios.get(url);
		return handleResponse(response);
	} catch (error) {
		handleError(error);
		return null;
	}
}

function handleResponse<T>(response: AxiosResponse<T>): T {
	if (response.status === 200) {
		const data: T = response.data;
		return data;
	} else {
		throw new Error('Network response was not ok.');
	}
}

// In a real app, would likely call an error logging service.
function handleError(error: any) {
	console.error('API call failed. ' + error);
}