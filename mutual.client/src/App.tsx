import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import Registro from './Pages/Registro';
import Login from './Pages/Login';

function App() {
    return (
        <Router>
            <nav>
                <ul>
                    <li><Link to="/">Inicio</Link></li>
                    <li><Link to="/registro">Registro</Link></li>
                    <li><Link to="/login">Login</Link></li>
                </ul>
            </nav>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/registro" element={<Registro />} />
                <Route path="/login" element={<Login />} />
            </Routes>
        </Router>
    );
}

function Home() {
    return (
        <div>
            <h1>Página de Inicio</h1>
            <p>Este es el contenido principal del proyecto React inicial.</p>
        </div>
    );
}

export default App;
