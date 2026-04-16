import { Routes, Route } from "react-router-dom";
import AuthorsPage from "./pages/Authors/AuthorsPage";
import Layout from "./components/Layout/Layout";

function App() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<AuthorsPage />} />
      </Route>
    </Routes>
  );
}

export default App;