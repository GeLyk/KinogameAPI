import http from 'k6/http';
import { sleep, check } from 'k6';

export let options = {
    thresholds: {
        http_req_duration: ['p(95)<500'], // Ο μέσος χρόνος απόκρισης πρέπει να είναι μικρότερος από 500ms για το 95% των αιτημάτων
        http_req_failed: ['rate<0.1'], // Το ποσοστό των αποτυχημένων αιτημάτων πρέπει να είναι μικρότερο από 10%
    },
    stages: [
        { duration: '20s', target: 50 }, 
        { duration: '30s', target: 50 }, 
        { duration: '10s', target: 0 }, 
    ],
};

export default function () {
    const url = 'https://localhost:44330/api/Ticket/CreateTicket';
    const payload = JSON.stringify({
            "createdOn": "2023-04-29T07:36:33.362Z",
            "lastModifiedOn": "2023-04-29T07:36:33.362Z",
            "id": 0,
            "price": 0,
            "profit": 0,
            "drawId": 0,
            "columns": [
                {
                    "createdOn": "2023-04-29T07:36:33.362Z",
                    "lastModifiedOn": "2023-04-29T07:36:33.362Z",
                    "id": 0,
                    "selectionNumbers": "string",
                    "selectionGame": 0,
                    "multiplier": 0,
                    "price": 0,
                    "kinoBonus": true,
                    "selectionRandom": true,
                    "cancel": true,
                    "profit": 0,
                    "success": 0
                }
            ]
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    let res = http.post(url, payload, params);

    check(res, {
        'status is 200': (r) => r.status === 200,
        'status is not 400': (r) => r.status !== 400,
        'status is not 500': (r) => r.status !== 500
    });

    sleep(1);
}
