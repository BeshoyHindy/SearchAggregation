# SearchAggregation

A web application that aggregates search results counts from multiple search engines. The application consists of a .NET backend API and a React frontend.

## Project Structure

The repository consists of two main parts:
- `API/` - Backend service written in C# (.NET)
- `Client/` - Frontend application built with React

## Features

- Input field for entering one or more search terms
- Aggregates search results from multiple search engines (Google, Bing)
- Shows total hit counts from each search engine
- Handles multi-word searches by summing individual word results
- Implements caching to improve performance
- Clean and responsive user interface

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v14 or later)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or VS Code
- API Keys for search engines:
  - Google Custom Search API Key
  - Bing Web Search API Key

## Getting Started

### Backend Setup

1. Navigate to the API folder:
```bash
cd API/SearchAggregation
```

2. Configure your API keys:
   - Open `appsettings.json`
   - Add your API keys for Google and Bing search services

3. Run the backend:
```bash
dotnet run
```
The API will start running on `https://localhost:5288`

### Frontend Setup

1. Navigate to the client folder:
```bash
cd Client/search-aggregator
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```
The frontend will be available at `http://localhost:5173`

## API Endpoints

### POST /api/search
- Accepts a JSON body with a query string
- Returns search results from multiple providers
```json
{
  "query": "search terms"
}
```

Response format:
```json
{
  "resultsByProvider": {
    "Google": 1000000,
    "Bing": 900000
  },
  "hasError": false,
  "errorMessage": null
}
```

## Technical Details

### Backend
- Built with .NET 9
- Uses dependency injection for service management
- Implements caching using IMemoryCache
- Handles concurrent requests efficiently
- Error handling and logging

### Frontend
- Built with React + Vite
- Uses Tailwind CSS for styling
- Implements shadcn/ui components
- Responsive design
- Loading states and error handling

## Error Handling

The application implements comprehensive error handling:
- Graceful degradation when search services are unavailable
- User-friendly error messages
- Backend logging for debugging
- Frontend loading states for better UX

## Caching

Search results are cached for 5 minutes to:
- Reduce API calls to search engines
- Improve response times
- Reduce costs

## Future Improvements

- Add more search providers
- Implement rate limiting
- Add user authentication
- Enhance error handling
- Add unit tests and integration tests
- Implement request validation
- Add search history feature

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request