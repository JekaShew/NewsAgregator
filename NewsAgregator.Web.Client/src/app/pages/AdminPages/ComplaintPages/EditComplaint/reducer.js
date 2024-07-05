import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINT_SELECT":
            return {
                ...state,
                editComplaint:
                {
                    ...state.editComplaint,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case 'COMPLAINT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editComplaint:
                {
                    ...state.editComplaint,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editComplaint[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "COMPLAINT_LOAD_SUCCESS":
            return {
                ...state,
                editComplaint:
                {
                    ...state.editComplaint,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}