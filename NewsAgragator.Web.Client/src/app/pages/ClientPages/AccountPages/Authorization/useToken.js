import { useState, useEffect } from 'react';



const useToken = () => {
  const [aToken, setAccessToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).aToken || '');
  const [rToken, setRefreshToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).rToken || '');

  const updateTokens = async (newAccessToken, newRefreshToken) => {
    setAccessToken(newAccessToken);
    setRefreshToken(newRefreshToken);
    localStorage.setItem("AUTHORIZATION", {aToken: newAccessToken, rToken :newRefreshToken});
  };

    // Асинхронная функция для обновления access token с помощью refresh token
    const refreshAccessToken = async () => {
        try {
          const response = await fetch('your-refresh-endpoint', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ refreshToken }),
          });
    
          if (response.ok) {
            const data = await response.json();
            updateTokens(data.accessToken, data.refreshToken);
          } else {
            console.error('Failed to refresh access token');
            // Обработка ошибки обновления токена, например, выход пользователя
          }
        } catch (error) {
          console.error('Error refreshing access token:', error);
        }
      };
    
      // Используем хук useEffect для автоматического обновления access token перед истечением его срока действия
      useEffect(() => {
        const interval = setInterval(() => {
          if (accessToken && refreshToken) {
            const expiresIn = new Date(accessToken.split('.')[1]).getTime();
            const currentTime = new Date().getTime();
            if (expiresIn - currentTime < 60000) { // Проверяем, осталось ли меньше минуты до истечения токена
              refreshAccessToken();
            }
          }
        }, 50000); // Проверяем каждые 50 секунд
    
        return () => clearInterval(interval);
      }, [accessToken, refreshToken]);
    
      // Возвращаем объект с текущими токенами и функциями для их обновления
      return { accessToken, refreshToken, updateTokens, refreshAccessToken };
    };