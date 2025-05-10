import React, { useState } from 'react';
import axios from 'axios';

const Login: React.FC = () => {
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [error, setError] = useState<string | null>(null);

    // Definir la interfaz para la respuesta
    interface LoginResponse {
        token: string;
    }

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        try {
            // Enviar solicitud al backend para obtener el token
            const response = await axios.post<LoginResponse>('http://localhost:5000/api/login', {
                username,
                password
            });

            // Guardar el token JWT en sessionStorage
            sessionStorage.setItem('token', response.data.token);

            // Redirigir a la p�gina de inicio
            window.location.href = '/home';
            } catch (error) {
                console.error(error);
                setError('Credenciales inv�lidas. Intenta nuevamente.');
            }
    };
;

    return (
        <div className="login-container">
            <h2>Iniciar sesi�n</h2>

            {error && <div style={{ color: 'red' }}>{error}</div>}

            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">Usuario</label>
                    <input
                        type="text"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="password">Contrase�a</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit">Iniciar sesi�n</button>
            </form>
        </div>
    );
};

export default Login;
