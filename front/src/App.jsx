import { Routes, Route } from "react-router-dom";
import AuthorsPage from "./pages/Authors/AuthorsPage";
import BooksPage from "./pages/Books/BooksPage";
import Layout from "./components/Layout/Layout";

function App() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<AuthorsPage />} />
        <Route path="/books" element={<BooksPage />} />
      </Route>
    </Routes>
  );
}

export default App;