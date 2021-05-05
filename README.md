# Diffing

Diffing is a test diffing library with web API and tests.

## Usage

Call GET/PUT on <host>/v1/diff/[ID]/[action] to set data or retrieve diff info.


### API Calls

  - [GET /v1/diff/[ID]]
  - [PUT /v1/diff/[ID]/left]
  - [PUT /v1/diff/[ID]/right]

## Request & Response Examples

### PUT /v1/diff/[ID]/left

Example: http://localhost/v1/diff/1/left

Request body:

{
    "data": "AAAAAA=="
}

Note: data is base64 encoded binary data

### PUT /v1/diff/[ID]/right

Example: http://localhost/v1/diff/1/right

Request body:

{
    "data": "AQABAQ=="
}

Note: data is base64 encoded binary data


### GET /v1/diff/[ID]

Example: http://localhost/v1/diff/1

Response body:

    {
        "diffResultType": "ContentDoNotMatch",
        "diffs": [
         {
             "offset": 0,
             "length": 1
         },
         {
             "offset": 2,
             "length": 2
         }
        ]
    }
