import { check, sleep } from "k6";
import http from "k6/http";

export let options = {
    vus: 10,
    stages: [
        { duration: "30s", target: 20 },
        { duration: "30s", target: 20 },
        { duration: "30s", target: 0 },
    ],
    thresholds: {
        "http_req_duration": ["p(95)<300"],
    },
    discardResponseBodies: false,
  cloud: {
    projectID: 5190904
  }
};

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min); 
}



export default function () {
    let res = http.get("bpcalculatortest-crdjejebh3dyazgq.francecentral-01.azurewebsites.net", { responseType: "text" });

    check(res, {
        "GET status 200": (r) => r.status === 200,
    });

    res = res.submitForm({
        fields: {
            Systolic: getRandomInt(90, 180).toString(),
            Diastolic: getRandomInt(60, 120).toString(),
            Age: getRandomInt(20, 80).toString(),
            Gender: "M"
        },
    });

    check(res, {
        "POST status 200": (r) => r.status === 200,
    });

    sleep(3);
}
