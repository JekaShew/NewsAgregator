export const add = (newWeather) => ({
    type: 'WEATHER_ADD',
    remote: {
        url: '/api/weather/add',
        type: 'post',
        body: newWeather,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedWeather) => ({
    type: 'WEATHER_SAVE',
    remote: {
        url: '/api/weather/update',
        type: 'post',
        body: changedWeather,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'WEATHER_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'WEATHER_SELECT',
    name: name,
    val: { id: value, text: text },
})

export const loadParameters = () => ({
    type: 'WEATHER_LOAD_PARAMETERS',
    remote: {
        url: '/api/weather/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'WEATHER_LOAD_PARAMETERS',
    remote: {
        url: '/api/weather/getparameters',
        type: 'get',
        apiSuccess: ['WEATHER_LOAD_PARAMETERS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'WEATHER_LOAD',
    remote: {
        url: '/api/weather/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'WEATHER_LOAD_SUCCESS',

    data:
    {
        id: '',
        city: {
            value: '',
        },
        temperatureMorning: {
            value: '',
        },
        temperatureDay: {
            value: '',
        },
        temperatureEvening: {
            value: '',
        },
        temperatureNight: {
            value: '',
        },
        temperatureCommon: {
            value: '',
        },
        date: '',
        percipitaion: {
            value: '',
        },
        wind: {
            value: '',
        },
        windDirection: {
            value: '',
        },
        pressure: {
            value: '',
        },
        humidity: {
            value: '',
        },

        weatherStatuses:'',
        weatherStatusMorning: '',
        weatherStatusDay: '',
        weatherStatusEvening: '',
        weatherStatusNight: '',
        weatherStatusCommon: '',
    }
})

