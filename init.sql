SELECT 'CREATE DATABASE account_db'
    WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'account_db')\gexec

\c account_db

CREATE SCHEMA IF NOT EXISTS demo;

CREATE TABLE IF NOT EXISTS demo.account (
    id bigint PRIMARY KEY GENERATED ALWAYS AS identity,
    owner varchar(100) NOT NULL,
    balance numeric NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS demo.purchase (
    id bigint PRIMARY KEY GENERATED ALWAYS AS identity,
    created timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    product varchar(100) NOT NULL,
    amount numeric NOT NULL,
    account_id bigint NOT NULL,
    CONSTRAINT fk_account
        FOREIGN KEY(account_id)
        REFERENCES account(id)
);

CREATE TABLE IF NOT EXISTS demo.sponsor_account (
    id bigint PRIMARY KEY GENERATED ALWAYS AS identity,
    owner varchar(100) NOT NULL,
    balance numeric NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS demo.sponsorship (
    id bigint PRIMARY KEY GENERATED ALWAYS AS identity,
    created timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    text varchar(100) NOT NULL,
    amount numeric NOT NULL,
    purchase_id bigint REFERENCES purchase (id),
    sponsor_account_id bigint REFERENCES sponsor_account (id)
);